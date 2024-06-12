
export interface CreateUserReq {
  firstName: string;
  lastName: string;
  dateOfBirth: string;
  joinedDate: string;
  gender: number;
  roleId: number;
  location: number;
}

export interface LoginReq {
  username: string;
  password: string;
}
