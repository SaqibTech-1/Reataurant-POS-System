import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, map, Observable, tap, throwError } from 'rxjs';
import { TokenStorage } from '../core/TokenStorage';
import { Credentials } from '../Interfaces/auth.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly baseUrl = 'http://localhost:5272/api/Auth';

  constructor(private http: HttpClient, private router: Router) { }

  // login(credentials:{userName: string;password: string}): Observable<any>{
  //   return this.http.post(`${this.baseUrl}/Login`, credentials).pipe(
  //     map((res: any) =>{
  //       console.log('Login response:', res);
  //       TokenStorage.saveToken(res.data.token);
  //     })
  //   );
  // }


  // logout():void{
  //   TokenStorage.clear();
  //   this.router.navigate(['/Login']);
  // }

  // isAuthenticated():boolean{
  //   return TokenStorage.isAuthenticated();
  // }

  // getUsername(): string | null {
  //   const token = TokenStorage.getToken();
  //   if (!token) return null;

  //   const payload = JSON.parse(atob(token.split('.')[1]));
  //   return payload?.UserName || null;
  // }

  getRoles(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Role`)
      .pipe(map(res => res));
  }


  login(credentials: Credentials): Observable<void> {
    return this.http
      .post<{ success: boolean; data: { token: string } }>(
        `${this.baseUrl}/Login`,
        credentials
      )
      .pipe(
        tap(res => TokenStorage.saveToken(res.data.token)),
        map(() => { })
      );
  }

  logout(): void {
    TokenStorage.clear();
    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {
    return TokenStorage.isAuthenticated();
  }


}
