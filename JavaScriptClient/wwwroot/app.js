function log() {
    document.getElementById('results').innerHTML = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (typeof msg !== 'undefined') {
            if (msg instanceof Error) {
                msg = 'Error: ' + msg.message;
            } else if (typeof msg !== 'string') {
                msg = JSON.stringify(msg, null, 2);
            }
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
};

// claims are global
let userClaims = null

async function startup() {
    var req = new Request("/bff/user", {
        headers: new Headers({
            'X-CSRF': '1'
        })
    })

    try {
        var resp = await fetch(req);
        if (resp.ok) {
            userClaims = await resp.json();

            log("user logged in", userClaims);
        }
        else if (resp.status === 401) {
            log("user not logged in");
        }
    }
    catch (e) {
        log("error checking user status");
    }
};

startup();

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("local").addEventListener("click", localApi, false);
document.getElementById("remote").addEventListener("click", remoteApi, false);
document.getElementById("logout").addEventListener("click", logout, false);


function login() {
    window.location = "/bff/login";
}

function logout() {
    if (userClaims) {
        if (userClaims.find(claim => claim.type === 'bff:logout_url')) {
            var logoutUrl = userClaims.find(claim => claim.type === 'bff:logout_url').value;
            window.location = logoutUrl;
        }
        else 
            window.location = "/bff/logout";
    }
    else {
        window.location = "/bff/logout";
    }
}


async function localApi() {
    var request = new Request("local/identity", {
        headers: new Headers({
            "X-CSRF": '1'
        })
    });

    try {

        var response = await fetch(request);
        if (response.ok) {
            var claims = await response.json();  
            log('user claims', claims);
        } else if (response.status === 401) {
            log('access denied');
        }
    } catch (ex) {
        
    }
}

async function remoteApi() {
    var request = new Request("/api/notes", {
        headers: new Headers({
            "X-CSRF": '1'
        })
    });

    try {

        var response = await fetch(request);
        if (response.ok) {
            var notes = await response.json();
            log('notes', notes);
        } else if (response.status === 401) {
            log('access denied');
        }
    } catch (ex) {

    }
}
