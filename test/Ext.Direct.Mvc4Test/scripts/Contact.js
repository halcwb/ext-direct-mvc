Ext.define('Contact', {
    extend: 'Ext.data.Model',
    idProperty: 'ID',
    fields: [
        { name: 'ID', type: 'int' },
        { name: 'FirstName', type: 'string' },
        { name: 'LastName', type: 'string' },
        { name: 'Email', type: 'string' },
        { name: 'BirthDate', type: 'date', dateFormat: 'c' },
        { name: 'Employed', type: 'boolean' }
    ]
});