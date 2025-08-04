export class TokenStorage{
    private static readonly TOKEN_KEY = 'access_token';

    static saveToken(token:string): void{
        localStorage.setItem(this.TOKEN_KEY, token);
    }

    static getToken():string | null{
        return localStorage.getItem(this.TOKEN_KEY);
    }

    static clear():void{
        localStorage.removeItem(this.TOKEN_KEY);
    }

    static isAuthenticated():boolean{
        return !!this.getToken();
    }

}