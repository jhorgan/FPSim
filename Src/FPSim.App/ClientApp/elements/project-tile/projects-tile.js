(function() {
    Polymer({
        is: 'project-tile',
        properties: {
            title: {
                type: String,
                value: "Project Name"
            },
            description: {
                type: String,
                value: "Project Description"
            },
            imageUrl: {
                type: String
            }
        }
    });
})();