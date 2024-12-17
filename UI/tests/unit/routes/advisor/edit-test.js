import { module, test } from 'qunit';
import { setupTest } from 'ui/tests/helpers';

module('Unit | Route | advisor/edit', function (hooks) {
  setupTest(hooks);

  test('it exists', function (assert) {
    let route = this.owner.lookup('route:advisor/edit');
    assert.ok(route);
  });
});
