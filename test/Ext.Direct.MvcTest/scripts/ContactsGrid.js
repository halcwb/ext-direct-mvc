Ext.define('ContactsGrid', {
    extend: 'Ext.grid.Panel',
    alias: 'widget.contactsgrid',
    
    title: 'Contacts',
    frame: true,
    
    columns: [
        { text: 'First Name', dataIndex: 'FirstName', flex: 1 },
        { text: 'Last Name', dataIndex: 'LastName', flex: 1 },
        { text: 'Birth Date', dataIndex: 'BirthDate', xtype: 'datecolumn', format: 'm/d/Y' },
        { text: 'Employed', dataIndex: 'Employed', xtype: 'booleancolumn', trueText: 'Yes', falseText: 'No', align: 'center' }
    ],
    
    initComponent: function () {
        this.store = Ext.create('Ext.data.Store', {
            model: 'Contact',
            proxy: {
                type: 'direct',
                directFn: Contacts.GetList,
                paramOrder: 'start|limit',
                reader: {
                    type: 'json',
                    root: 'data'
                }
            },
            pageSize: 10,
            autoLoad: true
        });

        this.dockedItems = [{
            xtype: 'pagingtoolbar',
            store: this.store,
            dock: 'bottom',
            displayInfo: true
        }];

        this.callParent();
    }
});