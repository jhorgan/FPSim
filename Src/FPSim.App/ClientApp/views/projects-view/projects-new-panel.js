(function () {
    Polymer({
        is: 'projects-new-panel',

        behaviors: [Reducers.ReduxBehavior],

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
            }
        },

        handleError: function (event, request) {
            const message = "Error creating the project " + this.projectName + ". " + event.detail.request.xhr.statusText;

            this._displayToast(message, true);
        },

        handleResponse: function (event, request) {

            const newProject = event.detail.response;

            // Add the new project to the local store once successfully saved
            const projectItem = {
                id: newProject.id,
                title: newProject.name,
                description: newProject.description,
                imageUrl: [appConfig.getApiUrl(), "/api/project/", newProject.id, '/image'].join("")
            };

            this.dispatch(ActionTypes.addProject(projectItem));

            this._displayToast("Created new project: " + newProject.name);
            this.$.panel.close();
        },

        open: function (event) {
            this._clearFields();
            this.$.panel.open();
        },

        _clearFields: function() {
            this.projectName = "";
            this.projectDescription = "";
        },

        _displayToast: function (message, isError) {

            if (typeof isError !== 'undefined' && isError) {
                this.$.toastMessage.duration = 5000;
                this.$.toastMessage.updateStyles({ '--paper-toast-background-color': '#a90f0f' });

                console.error(message);
            }

            this.$.toastMessage.text = message;
            this.$.toastMessage.open();
        }
    });
})();