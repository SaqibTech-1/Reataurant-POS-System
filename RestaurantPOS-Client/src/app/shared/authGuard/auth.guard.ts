import { Token } from '@angular/compiler';
import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
  UrlTree
} from '@angular/router';
import { Observable } from 'rxjs';
import { TokenStorage } from '../../auth/core/TokenStorage';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate{
   
    constructor(private router: Router){}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{
        if(TokenStorage.isAuthenticated()){
            return true;
        }
        this.router.navigate(['/login']);
        return false;
    }

}