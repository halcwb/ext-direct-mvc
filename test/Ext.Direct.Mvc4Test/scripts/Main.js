Ext.direct.Manager.addProvider(Ext.app.REMOTING_API);

Ext.application({
    launch: function () {
        // BASIC
        Ext.get('basicBtn').on('click', function () {
            var contact = {
                FirstName: 'John',
                LastName: 'Smith',
                BirthDate: new Date(1970, 6, 15),
                Employed: true
            };

            Basic.Echo("this is a basic demo", new Date(), contact, function (result, event) {
                var html;
                if (event.status == true) {
                    html = [
                        '<b>Echoed data</b><br/>',
                        'String: ' + result.text,
                        'Date: ' + Ext.Date.format(Ext.Date.parse(result.date, 'c'), 'l, F d, Y g:i:s A'),
                        'Contact: ' + Ext.encode(result.contact)
                    ].join('<br/>');
                    Ext.get('echoedData1').setDisplayed('block').update(html);
                }
            });
        });
        
        // NAMED ARGUMENTS
        Ext.get('namedArgsBtn').on('click', function () {
            var args = {
                contact: {
                    FirstName: 'John',
                    LastName: 'Smith',
                    BirthDate: new Date(1970, 6, 15),
                    Employed: true
                },
                date: new Date(),
                text: 'this is a named arguments demo'
            };

            Basic.EchoNamedArgs(args, function (result, event) {
                var html;
                if (event.status == true) {
                    html = [
                        '<b>Echoed data</b><br/>',
                        'String: ' + result.text,
                        'Date: ' + Ext.Date.format(Ext.Date.parse(result.date, 'c'), 'l, F d, Y g:i:s A'),
                        'Contact: ' + Ext.encode(result.contact)
                    ].join('<br/>');
                    Ext.get('echoedData2').setDisplayed('block').update(html);
                }
            });
        });

        // SERVER-SIDE EXCEPTION
        // BasicController.TestException method throws an exception.
        Ext.get('exceptionBtn').on('click', Basic.TestException);

        // exception event listener handles ALL server side exceptions and should only be set once in your code.
        Ext.direct.Manager.on('exception', function (error) {
            // error.where is present in Debug mode or if Ext.Direct.Mvc is configured with debug="true" in web.config.
            // error object can also contain any addition information that can help you with debugging. Check out BasicController.TestException.
            if (Ext.isDefined(error.where)) {
                // Detailed error message for developer
                console.error(Ext.util.Format.format('{0}\n{1}', error.message, error.where));
                Ext.Msg.show({
                    title: 'Error occured',
                    msg: Ext.util.Format.format('Exception was thrown from {0}.{1}.<br/>Check the console for details.', error.action, error.method),
                    icon: Ext.MessageBox.ERROR,
                    buttons: Ext.Msg.OK
                });
            } else {
                // User friendly message for end user
                Ext.Msg.show({
                    title: 'Error occured',
                    msg: 'Unable to process request. Please try again later.',
                    icon: Ext.MessageBox.ERROR,
                    buttons: Ext.Msg.OK
                });
            }
        });

        // GRID
        Ext.create('ContactsGrid', {
            width: 400,
            height: 300,
            renderTo: 'gridCt'
        });

        // FORM
        Ext.create('ContactForm', {
            width: 400,
            renderTo: 'formCt'
        });

        // TREE
        Ext.create('Tree', {
            width: 400,
            height: 300,
            renderTo: 'treeCt'
        });

        // FILE UPLOAD
        Ext.create('FileUploadForm', {
            width: 400,
            renderTo: 'fileFormCt'
        });
    }
});