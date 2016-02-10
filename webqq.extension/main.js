/**
 * Created by MC on 2016/2/4.
 */
jsT.startUp = function(){
    $('.nav_tab_head').append('<li id="qqextension" class="setup" > <a> <div class="icon"></div> <span>群发</span> </a> </li>');

    $('#qqextension').click(function(){
        jsT.qq.openGroupPanel();
    })

    //jsT.attachHtml('<div id="jst_entry" class="btn btn-danger">Go</div>', function(dm){   dm.click(function(){
    //    jsT.component['progressBar'].init();
    //});});

    jsT.qq.getFriends();
    //connection collection
    _(jsT.qq.model.friends).forEach(function (item) {
        item.markname = _.find(jsT.qq.model.marknames, { 'uin' : item.uin}).markname;
    });


    //jsT.qq.loadQQ = setInterval("collectionFirendInfo()", 1000)

}
function collectionFirendInfo() {
    if(jsT.qq.model != null) {
        _(jsT.qq.model.friends).forEach(function (item) {
            var getAccount = jsT.qq.getAccount(item.uin);
            getAccount.done(function (qq) {
                item.qqNumber = qq;
                console.log(item);
            });
        });
        window.clearInterval(jsT.qq.loadQQ);
    }
}

jsT.component = {
    progressBar : {
            dm : '<div id="jst_progress" class="progress"> ' +
            '<div class="progress-bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%;"> ' +
            '<span class="sr-only">60% Complete</span> </div> </div>',
            reset : function(){

            },
            init: function () {
                var ist = this.instance = $(this.dm);
                ist.css('width', '0%');
                $(document.body).append(ist);
                var i = 0;

                var a = setInterval('goss()', 1000);
                function goss(){
                    ist.css("width", (++i)+"%");
                }
            }
    }
}

