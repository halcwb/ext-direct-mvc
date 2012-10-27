Ext.direct.Manager.addProvider(Ext.app.REMOTING_API);

Ext.application({
    name: 'Mvc4Test',

    launch: function () {
        Ext.create('Ext.Button', {
            text: 'Echo current date and time',
            renderTo: 'viewport',
            handler: function () {
                var now = new Date();
                Test.EchoDateTime(now, function (result, event, status) {
                    var data = Ext.Date.format(Ext.Date.parse(result, 'c'), 'l, F d, Y g:i:s A');
                    Ext.Msg.alert('Echoed date and time', data);
                });
            }
        });
    }
});