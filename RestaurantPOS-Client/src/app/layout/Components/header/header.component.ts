import { Component } from '@angular/core';
import { LayoutService } from '../../Services/layout.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  user = {
    firstName: 'Uzair',
    lastName: 'Nasir',
    image: '' // Leave empty for now; later fill from login token/user service
  };
  userInitials: string = '';

  ngOnInit() {
    this.generateInitials();
  }

  generateInitials() {
    const { firstName, lastName } = this.user;
    this.userInitials = `${firstName[0] || ''}${lastName[0] || ''}`.toUpperCase();
  }

  constructor(public layout: LayoutService) {}

  toggleSidebar() {
    this.layout.toggleSidebar();
  }


}
