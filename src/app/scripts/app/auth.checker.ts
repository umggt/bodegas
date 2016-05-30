
module Bodega {

    var config = {
        authority: "http://localhost:5001/auth",
        client_id: "bodegas_app",
        redirect_uri: window.location.protocol + "//" + window.location.host + "/",
        post_logout_redirect_uri: window.location.protocol + "//" + window.location.host + "/logout.html",
        // these two will be done dynamically from the buttons clicked, but are
        // needed if you want to use the silent_renew
        response_type: "id_token token",
        scope: "openid profile email bodegas.api",
        // this will toggle if profile endpoint is used
        load_user_profile: false,
        // silent renew will get a new access_token via an iframe 
        // just prior to the old access_token expiring (60 seconds prior)
        silent_redirect_uri: window.location.protocol + "//" + window.location.host + "/silent_renew.html",
        silent_renew: false,
        // this will allow all the OIDC protocol claims to be visible in the window. normally a client app 
        // wouldn't care about them or want them taking up space
        filter_protocol_claims: false
    };

    var mgr = new OidcTokenManager(config);

    window.onmessage = (e) => {
        if (e.origin === window.location.protocol + "//" + window.location.host && e.data === "changed") {
            mgr.removeToken();
            //config.scope = "openid";
            //config.response_type = "id_token";
            //mgr.renewTokenSilentAsync().then(function () {
            //    console.log("renewTokenSilentAsync success");
            //}, function () {
            //    console.log("renewTokenSilentAsync failed");
            //});
        }
    }

    function checkSessionState() {
        mgr.oidcClient.loadMetadataAsync().then(meta => {

            const iframe = document.getElementById("rp") as HTMLIFrameElement;
            if (meta.check_session_iframe && mgr.session_state) {
                iframe.src = "check_session.html#" +
                    "session_state=" + mgr.session_state +
                    "&check_session_iframe=" + meta.check_session_iframe +
                    "&client_id=" + config.client_id
                    ;
            }
            else {
                iframe.src = "about:blank";
            }
        });
    }

    if (window.location.hash) {
        mgr.processTokenCallbackAsync().then(() => {
            var hash = window.location.hash.substr(1);
            var result = hash.split('&').reduce((result, item) => {
                var parts = item.split('=');
                result[parts[0]] = parts[1];
                return result;
            }, {});
            checkSessionState();
        }, error => {
            console.log("#response", error.message && { error: error.message } || error);
        });
    } else if (!mgr.access_token || !mgr.id_token || !mgr.profile || mgr.expired) {
        mgr.redirectForToken();
    }

    export var tokenManager = mgr;
}