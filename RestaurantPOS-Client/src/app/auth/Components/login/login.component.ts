import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../Services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm!: FormGroup;
  submitted = false;
  loginError: string = '';
  loading = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private toaster: ToastrService
  ) {
    this.loginForm = this.fb.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  get f() {
    return this.loginForm.controls;
  }

  onSubmit(): void {
    // this.submitted = true;
    // this.loginError = '';
    // this.loginForm.markAllAsTouched();

    // if (this.loginForm.invalid) return;

    // this.authService.login(this.loginForm.value).subscribe({
    //   next: () => this.router.navigate(['/dashboard']),
    //   error: err => {
    //     this.loginError = err.error?.message || 'Login failed. Check your credentials.';
    //   }
    // });


    this.submitted = true;
    this.loginError = '';
    this.loginForm.markAllAsTouched();

    if (this.loginForm.invalid) return;

    this.loading = true;

    this.authService.login(this.loginForm.value).subscribe({
      next: () => {
        // Simulate small delay for better UX
        setTimeout(() => {
          this.loading = false;
          this.toaster.success('Login successful!');
          setTimeout(() => {
            this.router.navigate(['/dashboard']);
          }, 1000);
        }, 1000); // 1 second delay
      },
      error: err => {
        this.loading = false;
        this.loginError = err.error?.message || 'Login failed. Check your credentials.';
        this.toaster.error(this.loginError);
      }
    });
  }
}
