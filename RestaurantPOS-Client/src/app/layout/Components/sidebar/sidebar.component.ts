import { Component } from '@angular/core';
import { LayoutService } from '../../Services/layout.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
   isCollapsed = false;

  // Track menu open/close state
  menuStates: Record<string, boolean> = {
    users: false,
    products: false,
    categories: false,
  };

  constructor(private layout: LayoutService) {
    this.layout.collapsed$.subscribe((collapsed) => {
      this.isCollapsed = collapsed;
    });
  }

  toggleMenu(menu: keyof typeof this.menuStates) {
    this.menuStates[menu] = !this.menuStates[menu];
  }
}
