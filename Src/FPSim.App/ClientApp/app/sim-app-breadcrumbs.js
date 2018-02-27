(function() {
    Polymer({
        is: 'sim-app-breadcrumbs',
        properties: {
            projectsUrl: {
                type: String,
                value: function () {
                    return [appConfig.getApiUrl(), "/api/user/", appConfig.getCurrentUserId(), "project"].join("")
                }
            },
            breadcrumbItems: {
                type: Object
            },
            selectedRoute: {
                type: Object
            }
        },

        handleError: function (event, request) {
            // TODO: handle error for user
            console.log("Error getting the projects and scenarios. " + event.detail.request.xhr.statusText);
        },

        handleResponse: function (event, request) {
            const projects = event.detail.response;

            var projectItems = projects.map(function (project) {
                return {
                    label: project.name,
                    id: project.id,
                    children: []
                }
            });

            // Add a "blank" entry if no items are loaded
            if (projectItems.length == 0) {
                projectItems.push({ label: "", id: "empty", children: [] });
            }

            this.breadcrumbItems = projectItems;
            this.selectedRoute = [projectItems[0].id]
        },
    });
})()