(function () {
    Polymer({
        is: 'projects-view',

        behaviors: [Reducers.ReduxBehavior],

        properties: {
            projectsUrl: {
                type: String,
                value: function () {
                    return [appConfig.getApiUrl(), "/api/project/user/", appConfig.getCurrentUserId()].join("")
                }
            },
            projects: {
                type: Object,
                statePath: "projects"
            },

            fetching: {
                type: Boolean,
                statePath: "isFetching"
            }
        },

        handleAdd: function (event) {
            this.$.newProjectPanel.open();
        },

        handleError: function (event, request) {
            const message = "Error getting the projects for projects view. " + event.detail.request.xhr.statusText;

            this._displayToast(message, true);
        },

        handleResponse: function (event, request) {
            const projects = event.detail.response;

            // Adapt the project items from the Api call to data items 
            // that are rendered in this view
            var projectItems = projects.map(function (project) {
                return {
                    id: project.id,
                    title: project.name,
                    description: project.description,
                    imageUrl: [appConfig.getApiUrl(), "/api/project/", project.id, '/image'].join("")
                }
            });

            this.dispatch(ActionTypes.storeProjects(projectItems));
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