import { module, test } from 'qunit';
import { setupTest } from 'ui/tests/helpers';

module('Unit | Controller | advisor/index', function (hooks) {
  setupTest(hooks);

  // TODO: Replace this with your real tests.
  test('it exists', function (assert) {
    let controller = this.owner.lookup('controller:advisor/index');
    assert.ok(controller);
  });
});
