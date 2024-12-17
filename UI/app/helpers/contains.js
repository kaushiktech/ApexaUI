import { helper } from '@ember/component/helper';

function contains([str, substr]) {
  return str.includes(substr);
}

export default helper(contains);
