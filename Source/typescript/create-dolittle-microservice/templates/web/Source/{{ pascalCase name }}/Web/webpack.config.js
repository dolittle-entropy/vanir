const webpack = require('@dolittle/vanir-webpack/frontend');
module.exports = (env, argv) => {
    return webpack(env, argv, '{{lowerCase uiPath}}', config => {
        config.devServer.proxy = {
            '{{lowerCase uiPath}}/graphql': 'http://localhost:3000',
            '/api': 'http://localhost:3000'
        };
    }, 9000, '{{pascalCase applicationName}}');
};
