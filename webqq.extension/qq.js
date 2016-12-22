/**
 * Created by MC on 2016/2/4.
 */
jsT.qq = {};
jsT.qq.views = {};
var buildPanel = function(){
    var tpl = '<div class="panel chat-panel" id="panel-999" cmd="void" style="transition: -webkit-transform 0.4s cubic-bezier(0, 1, 0, 1); transform: translate3d(0px, 0px, 0px); display: block;"> <header id="panelHeader-999" class="panel_header"> <div id="panelLeftButton-999" class="btn btn_small btn_left btn_black btn_setting" cmd="clickLeftButton"> </div> <h1 id="panelTitle-999" class="text_ellipsis padding_20">按分组群发</h1> <button id="panelRightButton-999" class="btn btn_small btn_right btn_black btn_setting" cmd="clickRightButton"> <span id="panelRightButtonText-999" class="btn_text">关闭</span> </button> </header> <div id="panelBodyWrapper-999" class="panel_body_container" style="top: 45px; bottom: 95px; overflow: hidden;"> <div id="panelBody-999" class="panel_body chat_container" style="transition-property: -webkit-transform; transform-origin: 0px 0px 0px; transform: translate(0px, 0px) scale(1) translateZ(0px);"></div> </div> <footer id="panelFooter-999" class="chat_toolbar_footer"> <div class="chat_status" style="width: 0%; position: absolute; height: 5px; background-color: red;"></div> <div class="chat_toolbar"> <textarea id="chat_textarea999" class="input input_white chat_textarea" style="height:320px;overflow-y: scroll;"></textarea> <button id="send_chat_btn_999" class="btn btn_small btn_blue" cmd="sendMsg"> <span class="btn_text">发送</span> </button> <textarea id="" class="input input_white chat_textarea hidden_textarea" style="height: 32px; width: 99964px;"></textarea> </div> </div> </footer> </div>';
    if(jsT.qq.views.groupSend != null)
    {
        jsT.qq.views.groupSend.toggle();
    }
    else
    {
        jsT.qq.views.groupSend = $(tpl);
        $('#container').append(jsT.qq.views.groupSend);
        //创建下拉框
        var groups = jsT.qq.model.categories;
        groups = _.sortBy(groups, ['sort']);
        var select = '<select id="groups-999">' ;
        for(var i = 0; i < groups.length ; i ++){
            select += '<option value="'  + groups[i].index + '">'+ groups[i].name+ '</option>'
        }
        select += '<option value="-1" selected> 请选择分组</option>'
        select += '</select>';
        $("#panelLeftButton-999").append(select);
    }
    $('#panelRightButton-999').click(function(){
        jsT.qq.views.groupSend.hide();
    })

    //发送消息
    $('#send_chat_btn_999').click(function(){
        var group = parseInt($('#groups-999').val());
        if (group > -1){
        var content = $('#chat_textarea999').val();
        var msgDom = '<div class="chat_content_group self  " _sender_uin="{0}"> <img class="chat_content_avatar" src="http://face3.web.qq.com/cgi/svr/face/getface?cache=1&amp;type=1&amp;f=40&amp;uin={0}&amp;t=1455020440&amp;vfwebqq={1}" width="40px" height="40px"> <p class="chat_nick">{2}</p> <p class="chat_content ">{3}</p> </div>';
        var selfinfo = jsT.qq.getSelfInfo();
        msgDom = msgDom.format(selfinfo.uin, mq.vfwebqq, selfinfo.nick, content);
        $('#panelBody-999').append(msgDom);
        $('#chat_textarea999').val('');
        jsT.qq.sendMessageToGroup(group, content);
        } else {
            alert("请选择群组");
        }
    })

}

jsT.qq.openGroupPanel = function(){
    buildPanel();
};

//发消息
jsT.qq.sendMessage = function(toUin, msgBody){
    mq.rpcService.require({
        url: 'http://d.web2.qq.com/channel/send_buddy_msg2',
        https: mq.setting.enableHttps,
        param: {"to":toUin,"content":"[\"" + msgBody + "\",[\"font\",{\"name\":\"宋体\",\"size\":10,\"style\":[0,0,0],\"color\":\"000000\"}]]","face":522,"clientid":53999199,"msg_id":22660007,"psessionid": mq.psessionid},
        withCredentials: true,
        method: 'POST',
        onSuccess: function () {
            console.log("success");
        }
    })
}

//获取账号信息
jsT.qq.getAccount = function(toUin){
    var dtd = $.Deferred();
    var qq = null;
    mq.rpcService.require({
        url: 'http://s.web2.qq.com/api/get_friend_uin2',
        method: 'GET',
        param: { tuin : toUin, type : 1, vfwebqq: mq.vfwebqq},
        withCredentials: true,
        onSuccess: function (r) {
            dtd.resolve(r.result.account);
        },
        onError : function(r){
            dtd.reject();
        }
    })
    return dtd;
}
jsT.qq.sendMsgAnsyc = function(uin, msg, width){
    jsT.qq.sendMessage(uin, msg);
    $('.chat_status').width(width);
}
jsT.qq.sendMessageToGroup = function(catId, msg){
    var chatStatus = $('.chat_status');
    if(chatStatus.width() != 0)
    {
        alert('有正在群发的任务，稍后再试!'); return;
    }
    var allMembers = _.filter(jsT.qq.model.friends, { 'categories': catId});

    for(var i = 0; i < allMembers.length ; i ++){
        //jsT.qq.sendMessage(allMembers[i].uin, msg);
        //chatStatus.width(((i+1)/allMembers.length * 100) + '%')
        setTimeout('jsT.qq.sendMsgAnsyc({0},"{1}","{2}%");'.format(allMembers[i].uin, msg,((i+1)/allMembers.length * 100)), i*1000);
    }
    setTimeout('$(".chat_status").width(0);', allMembers.length *1000);
}
jsT.qq.model = {};
//获取
jsT.qq.getFriends = function(){
    mq.rpcService.require({
    url: 'http://s.web2.qq.com/api/get_user_friends2',
    method: 'POST',
    withCredentials: true,
    param: {"vfwebqq": mq.vfwebqq, "hash":jsT.qq.u()},
    onSuccess: function (r) {
        console.log(r.result);
        jsT.qq.model = r.result;
    }
});
}

//获取自己的uin
jsT.qq.getSelfUin = function(){
    return mq.model.buddylist.getSelfUin();
}

jsT.qq.getSelfInfo = function(){
    return mq.model.buddylist.getSelfInfo();
}
jsT.qq.u = function(c){

    var u = function (x, I) {
        x += '';
        for (var N = [
        ], T = 0; T < I.length; T++) N[T % 4] ^= I.charCodeAt(T);
        var U = [
                'EC',
                'OK'
            ],
            V = [
            ];
        V[0] = x >> 24 & 255 ^ U[0].charCodeAt(0);
        V[1] = x >> 16 & 255 ^ U[0].charCodeAt(1);
        V[2] = x >> 8 & 255 ^ U[1].charCodeAt(0);
        V[3] = x & 255 ^ U[1].charCodeAt(1);
        U = [
        ];
        for (T = 0; T < 8; T++) U[T] = T % 2 == 0 ? N[T >> 1] : V[T >> 1];
        N = [
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9',
            'A',
            'B',
            'C',
            'D',
            'E',
            'F'
        ];
        V = '';
        for (T = 0; T < U.length; T++) {
            V += N[U[T] >> 4 & 15];
            V += N[U[T] & 15]
        }
        return V
    }
    return u(mq.model.buddylist.getSelfUin(), mq.ptwebqq);
}

