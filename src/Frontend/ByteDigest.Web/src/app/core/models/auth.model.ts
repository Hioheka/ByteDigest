export interface LoginRequest {
  userNameOrEmail: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  userName: string;
  email: string;
  role: string;
}

export interface User {
  userName: string;
  email: string;
  role: string;
}
