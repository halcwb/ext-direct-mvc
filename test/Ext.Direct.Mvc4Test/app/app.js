Ext.direct.Manager.addProvider(Ext.app.REMOTING_API);

Ext.application({
    name: 'Movies',

    launch: function () {

        Ext.define('Movie', {
            extend: 'Ext.data.Model',
            idProperty: 'ID',
            fields: [
                { name: 'ID', type: 'int' },
                { name: 'Title', type: 'string' },
                { name: 'ReleaseDate', type: 'date', format: 'c' }
            ]
        });

        var movieStore = Ext.create('Ext.data.Store', {
            model: 'Movie',
            proxy: {
                type: 'direct',
                directFn: Movies.GetAll,
                reader: {
                    type: 'json'
                }
            },
            autoLoad: true
        });

        var movieGrid = Ext.create('Ext.grid.Panel', {
            title: 'Movies',
            width: 600,
            height: 300,
            frame: true,
            store: movieStore,
            columns: [
                { text: 'Title', dataIndex: 'Title', flex: 1 },
                { text: 'Release Date', dataIndex: 'ReleaseDate', xtype: 'datecolumn', format: 'd F Y', width: 120 }
            ],
            renderTo: 'viewport'
        });
    }
});