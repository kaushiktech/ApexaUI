import { module, test } from 'qunit';
import { setupTest } from 'ui/tests/helpers';

module('Unit | Service | pop-up', function (hooks) {
  setupTest(hooks);

  // TODO: Replace this with your real tests.
  test('it exists', function (assert) {
    let service = this.owner.lookup('service:pop-up');
    assert.ok(service);
  });
});
