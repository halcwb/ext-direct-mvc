Ext.define('ContactForm', {
    extend: 'Ext.form.Panel',
    title: 'Contact',
    frame: true,
    bodyPadding: 10,
    defaults: {
        anchor: '100%',
        labelWidth: 80
    },
    items: [
        { xtype: 'textfield', name: 'FirstName', fieldLabel: 'First Name', allowBlank: false },
        { xtype: 'textfield', name: 'LastName', fieldLabel: 'Last Name', allowBlank: false },
        { xtype: 'datefield', name: 'BirthDate', fieldLabel: 'Birth Date', format: 'm/d/Y', altFormats: 'c', allowBlank: false },
        { xtype: 'checkboxfield', name: 'Employed', fieldLabel: 'Employed', inputValue: 'true' }
    ],

    initComponent: function () {
        var config = {
            api: {
                load: Contacts.Get,
                submit: Contacts.Update
            },
            paramOrder: 'id',
            buttons: [{
                text: 'Load Contact',
                handler: this.loadContact,
                scope: this
            }, {
                text: 'Update Contact',
                itemId: 'updateButton',
                disabled: true,
                handler: this.updateContact,
                scope: this
            }],
            buttonAlign: 'center'
        };

        Ext.apply(this, config);

        this.callParent();
    },
    
    loadContact: function () {
        this.getForm().load({
            params: {
                id: 1
            },
            success: function () {
                this.down('#updateButton').enable();
            },
            scope: this
        });
    },
    
    updateContact: function () {
        this.getForm().submit({
            params: {
                id: 1
            },
            success: function () {
                alert('Contact updated!');
            }
        });
    }
});