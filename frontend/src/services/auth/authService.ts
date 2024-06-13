import axiosInstance from "@/api/axiosInstance";
import { FirstTimeLoginReq, LoginReq } from "@/models";

export const loginService = (req: LoginReq) => {
  return axiosInstance
    .post("/auth", req)
    .then((res) => {
      localStorage.setItem("token", res.data.token);
      return {
        success: true,
        message: "Login successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return { success: false, message: "Failed to login.", error: err };
    });
};

export const firstTimeService = (req: FirstTimeLoginReq) => {
  return axiosInstance
    .post("/auth/first-time", req.newPassword)
    .then((res) => {
      return {
        success: true,
        message: "Change password successfully!",
        data: res.data,
      };
    })
    .catch((err) => {
      return {
        success: false,
        message: "Failed to change password.",
        error: err,
      };
    });
};