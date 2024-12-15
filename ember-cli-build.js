'use strict';

const EmberApp = require('ember-cli/lib/broccoli/ember-app');

module.exports = function (defaults) {
  const app = new EmberApp(defaults, {
    'ember-cli-babel': { enableTypeScriptTransform: true },
    sassOptions: {
      includePaths: ['node_modules/bootstrap/scss'],
    },
    // Add options here
  });
  app.import('node_modules/bootstrap/dist/js/bootstrap.bundle.min.js');
  return app.toTree();
};
