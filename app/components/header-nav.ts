import Component from '@glimmer/component';
import { inject as service } from '@ember/service';
import eq from 'ember-truth-helpers/helpers/eq'
import type RouterService from '@ember/routing/router-service';
export interface HeaderNavSignature {}

export default class HeaderNav extends Component<HeaderNavSignature> {
  @service declare router: RouterService;

  get activeRoute():string {
    return this.router.currentRouteName==null? "": this.router.currentRouteName;
  }
}
