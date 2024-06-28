import { AssignmentRes } from "@/models";
import { getAssignmentByUserAssignedService } from "@/services/admin/manageAssignmentService";
import { useEffect, useState } from "react";

export const useMyAssignments = (
  pagination: {
    pageSize: number;
    pageIndex: number;
  },
  userId: string,
  search?: string,
  orderBy?: string,
  isDescending?: boolean,
  assignmentState?: number,
  assignedDate?: Date,
) => {
  const [assignments, setAssignments] = useState<AssignmentRes[] | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<boolean | null>(false);
  const [pageCount, setPageCount] = useState<number>(0);
  const [totalRecords, setTotalRecords] = useState<number>(0);

  const fetchAssignments = async () => {
    setLoading(true);
    try {
      const data = await getAssignmentByUserAssignedService({
        pagination,
        search,
        orderBy,
        isDescending,
        assignmentState,
        assignedDate,
        userId,
      });

      setAssignments(data.data.data);
      setPageCount(data.data.totalPages);
      setTotalRecords(data.data.totalRecords);
    } catch (error) {
      setError(true);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchAssignments();
  }, [
    pagination,
    search,
    orderBy,
    isDescending,
    userId,
    assignedDate,
    assignmentState,
  ]);

  return {
    assignments,
    loading,
    error,
    setAssignments,
    pageCount,
    totalRecords,
    fetchAssignments,
  };
};
