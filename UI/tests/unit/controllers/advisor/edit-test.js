import { module, test } from 'qunit';
import { setupTest } from 'ui/tests/helpers';

module('Unit | Controller | advisor/edit', function (hooks) {
  setupTest(hooks);

  // TODO: Replace this with your real tests.
  test('it exists', function (assert) {
    let controller = this.owner.lookup('controller:advisor/edit');
    assert.ok(controller);
  });
});
