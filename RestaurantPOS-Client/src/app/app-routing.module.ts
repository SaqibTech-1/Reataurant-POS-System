import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/Components/login/login.component';
import { LayoutComponent } from './layout/Components/layout/layout.component';
import { DashboardComponent } from './layout/Components/dashboard/dashboard.component';
import { AuthGuard } from './shared/authGuard/auth.guard';

// const routes: Routes = [
//   { path: 'login', component: LoginComponent },
//   { path: '', component: LayoutComponent,
//     children:[
//         {path:'dashboard', component:DashboardComponent}
//     ]
//   },
// ];

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      // { path: 'users/create', component: UserCreateComponent },
      // { path: 'users/list', component: UserListComponent },
      // { path: 'products/create', component: ProductCreateComponent },
      // { path: 'products/list', component: ProductListComponent },
      // { path: 'categories/create', component: CategoryCreateComponent },
      // { path: 'categories/list', component: CategoryListComponent },
    ],
  },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
