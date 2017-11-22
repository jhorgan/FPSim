(function () {
    Polymer({
        is: 'projects-new-panel',

        properties: {
            projectName: String,
            projectDescription: String,
            postUrl: {
                type: String,
                value: function () {
                    return [appConfig.getApiUrl(), "/api/project"].join("")
                }
            },
            isPosting: {
                type: Boolean,
                notify: true,
                value: false
            }
        },

        handleCancel: function (event) {
            this.$.panel.close();
        },

        handleOK: function (event) {
           
            if (this.$.projectName.validate()) {
                this.$.postProject.body = {
                    name: this.projectName,
                    description: this.projectDescription,
                    applicationId: appConfig.getCurrentAppId(),
                    userId: appConfig.getCurrentUserId()
                };
                this.$.postProject.generateRequest();

                // TODO: refresh parent view
            }
        },

        handleError: function (event, request) {
            const message = "Error creating the project " + this.projectName + ". " + event.detail.request.xhr.statusText;

            this._displayToast(message, true);            
        },

        handleResponse: function (event, request) {

            this._displayToast("Created new project: " + this.projectName);
            this.$.panel.close();
        },

        open: function (event) {
            this.$.panel.open();
        },

        _displayToast: function(message, isError) {

            if (typeof isError !== 'undefined' && isError) {
                this.$.toastMessage.duration = 5000;
                this.$.toastMessage.updateStyles({'--paper-toast-background-color': '#a90f0f'});

                console.log(message);
            }

            this.$.toastMessage.text = message;
            this.$.toastMessage.open();
        }
    });
})();