import { Component } from '@angular/core';
import { AuthService } from './auth/Services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'RestaurantPOS-Client';

  username: string | null = '';

  constructor(private auth: AuthService) {
    // this.username = auth.getUsername();
  }

  logout() {
    this.auth.logout();
  }

}
