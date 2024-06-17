import { UserRes } from "@/models";
import { getAllUserService } from "@/services";
import { useEffect, useState } from "react";

export const useUsers = (
  token: string,
  pageNumber: number,
  pageSize: number,
) => {
  const [users, setUsers] = useState<UserRes[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<boolean | null>(false);

  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const data = await getAllUserService({ token, pageNumber, pageSize });
        console.log(data);

        setUsers(data.data.data);
      } catch (error) {
        setError(true);
      } finally {
        setLoading(false);
      }
    };

    fetchUsers();
  }, [token, pageNumber, pageSize]);

  return { users, loading, error };
};
