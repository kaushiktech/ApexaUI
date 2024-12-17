import Component from '@glimmer/component';
import { action } from '@ember/object';
export default class Healthbar extends Component {
  @action
  getHealthColor(health) {
    let color = '';
    switch (health) {
      case 'yellow':
        color = 'warning';
        break;
      case 'green':
        color = 'success';
        break;
      case 'red':
        color = 'danger';
        break;
    }
    return color;
  }
}
