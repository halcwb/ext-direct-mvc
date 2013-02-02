Ext.define('FileUploadForm', {
    extend: 'Ext.form.Panel',
    
    title: 'File Upload Form',
    frame: true,
    bodyPadding: 10,

    defaults: {
        xtype: 'filefield',
        anchor: '100%',
        labelWidth: 80
    },
    items: [
        { fieldLabel: 'File #1' },
        { fieldLabel: 'File #2' },
        { fieldLabel: 'File #3' }
    ],

    initComponent: function () {
        var config = {
            api: {
                submit: Files.Upload
            },
            buttons: [{
                text: 'Upload Files',
                handler: this.uploadFiles,
                scope: this
            }],
            buttonAlign: 'center'
        };

        Ext.apply(this, config);

        this.callParent();
    },

    uploadFiles: function () {
        this.getForm().submit({
            success: function (form, action) {
                Ext.Msg.alert('Done', 'Files uploaded successfully!<br/>You can find them in "Uploaded Files" folder.');
            }
        });
    }
});