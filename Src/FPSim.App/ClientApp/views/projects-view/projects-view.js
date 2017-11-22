(function () {
    Polymer({
        is: 'projects-view',

        properties: {
            projectsUrl: {
                type: String,
                value: function () {
                    return [appConfig.getApiUrl(), "/api/project/user/", appConfig.getCurrentUserId()].join("")
                }
            },
            projects: {
                type: Object
            }
        },
        
        handleAdd: function (event) {
            this.$.newProjectPanel.open();
        },

        handleError: function (event, request) {
            // TODO: handle error for user
            console.log("Error getting the projects for projects view. " + event.detail.request.xhr.statusText);
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

            this.projects = projectItems;
        }
    });
})();