(function () {
    Polymer({
        is: 'scenarios-view',

        behaviors: [Reducers.ReduxBehavior],

        properties: {
            scenarionsUrl: {
                type: String,
                value: function () {
                    return [appConfig.getApiUrl(), "/api/user/", appConfig.getCurrentUserId(), "/project/", projectId, "/scenario"].join("")
                }
            },
            scenarios: {
                type: Object,
                statePath: "scenarios"
            }
        },

        handleAdd: function (event) {
            // this.$.newProjectPanel.open();
        },

        handleError: function (event, request) {
            const message = "Error getting the scenarios for scenarios view. " + event.detail.request.xhr.statusText;

            this._displayToast(message, true);
        },

        handleResponse: function (event, request) {
            const scenarios = event.detail.response;

            // Adapt the scenarios from the Api call to data items 
            // that are rendered in this view
            var scenarioItems = scenarios.map(function (scenario) {
                return {
                    id: scenario.id,
                    title: scenario.name,
                    description: scenario.description
                    // imageUrl: [appConfig.getApiUrl(), "/api/project/", project.id, '/image'].join("")
                }
            });

            this.dispatch(ActionTypes.storeScenarios(scenarioItems));
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