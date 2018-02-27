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
                    return [appConfig.getApiUrl(), "/api/user/", appConfig.getCurrentUserId(), "/project"].join("")
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

        handleFileChanged: function () {
            console.log("handleFileChanged");
            // TODO: not working
            var that = this;
            if (this.$.fileUpload.files.length > 0) {
                var reader = new FileReader();
                reader.onload = function () {
                    that.$.projectImage.src = reader.result;
                }
                reader.readAsDataURL(this.$.fileUpload.files[0]);
            }
            else {
                this.$.projectImage.src = null;
            }
        },

        handleOK: function (event) {

            if (this.$.projectName.validate()) {
                var that = this;

                // saveFunction invoked when the image is loaded (assuming one was entered)
                var saveFunction = function (arrayBuffer) {
                    that.$.postProject.body = {
                        name: that.projectName,
                        description: that.projectDescription,
                        applicationId: appConfig.getCurrentAppId(),
                        userId: appConfig.getCurrentUserId(),
                        image: Utils.base64ArrayBuffer(arrayBuffer)
                    };
                    that.$.postProject.generateRequest();
                }

                var reader = new FileReader();
                reader.onloadend = saveFunction;
                reader.onerror = saveFunction;

                // If a file was selected, attempt to load it
                if (this.$.fileUpload.files.length > 0) {
                    reader.onloadend = function () {
                        saveFunction(reader.result);
                    }
                    reader.onerror = function () {
                        saveFunction();
                    }
                    reader.readAsArrayBuffer(this.$.fileUpload.files[0]);
                }
                else {
                    saveFunction();
                }
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
                imageUrl: [appConfig.getApiUrl(), "/api/user/", appConfig.getCurrentUserId(), "/project/", newProject.id, '/image'].join("")
            };

            this.dispatch(ActionTypes.addProject(projectItem));

            this._displayToast("Created new project: " + newProject.name);
            this.$.panel.close();
        },

        open: function (event) {
            this._clearFields();
            this.$.panel.open();
        },

        _clearFields: function () {
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