export interface CreateUserReq {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  joinedDate: string;
  gender: number;
  role: number;
  location: number;
}

export interface LoginReq {
  username: string;
  password: string;
}

export interface FirstTimeLoginReq {
  username: string;
  oldPassword: string;
  newPassword: string;
}
