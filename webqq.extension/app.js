/**
 * Created by MC on 2016/2/4.
 */
var hostAddress = 'http://localhost:63342/webqq.extension/';
if(jsT != null){
    alert("代码已经安装！");
}else{
//javascript:var q = document.createElement('script');q.src = "http://localhost:63342/webqq.extension/app.js";document.body.appendChild(q);
var jsT = window.jsT = {};
jsT.rs = {
    appCss : hostAddress + 'app.css',
    bsCss : hostAddress + 'lib/bootstrap.min.css',
    qqJs : hostAddress + 'qq.js',
    sqqJs : hostAddress + 'lib/qq_.js',
    lodashJs : hostAddress + 'lib/lodash.js',
    mainJs : hostAddress + 'main.js',
    jqJs : hostAddress + 'lib/jquery.min.js',
    bsJs : hostAddress + 'lib/bootstrap.min.js'
}

jsT.usingCss =   function(path){
            if(!path || path.length === 0){
                throw new Error('argument "path" is required !');
            }
            var head = document.getElementsByTagName('head')[0];
            var link = document.createElement('link');
            link.href = path;
            link.rel = 'stylesheet';
            link.type = 'text/css';
            head.appendChild(link);
        };

jsT.usingJs = function(path){
            if(!path || path.length === 0){
                throw new Error('argument "path" is required !');
            }
            var head = document.getElementsByTagName('head')[0];
            var script = document.createElement('script');
            script.src = path;
            script.type = 'text/javascript';
            head.appendChild(script);
};

//jsT.usingJs(jsT.rs['sqqJs']);
jsT.usingJs(jsT.rs['jqJs']);
jsT.usingJs(jsT.rs['lodashJs']);
//jsT.usingCss(jsT.rs['bsCss']);
jsT.usingCss(jsT.rs['appCss']);
jsT.usingJs(jsT.rs['qqJs']);
jsT.usingJs(jsT.rs['mainJs']);


jsT.attachHtml = function(htmStr, bindEvent){
    var dm = $(htmStr);
    $(document.body).append(dm);
    bindEvent && bindEvent(dm);
}

jsT.init = setInterval('main()', 1000)
// the entry port of program
function main(){
    if(jQuery != undefined && jsT.startUp != null){
        jsT.usingJs(jsT.rs['bsJs']);
        window.clearInterval(jsT.init)
        jsT.startUp();
    }
}
}

