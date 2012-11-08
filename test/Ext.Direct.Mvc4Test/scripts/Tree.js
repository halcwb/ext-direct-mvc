Ext.define('Tree', {
    extend: 'Ext.tree.Panel',
    
    title: 'Tree',
    frame: true,

    tbar: [{
        text: 'Reload',
        itemId: 'reloadButton'
    }],

    initComponent: function () {
        this.store = Ext.create('Ext.data.TreeStore', {
            root: {
                expanded: true
            },
            proxy: {
                type: 'direct',
                directFn: Tree.Load,
                paramOrder: 'node'
            }
        });

        this.callParent();

        this.down('#reloadButton').on('click', function () {
            this.getStore().load();
        }, this);
    }
});