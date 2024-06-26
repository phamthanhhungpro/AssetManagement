import { useAuth } from "@/hooks";
import { renderHeader } from "@/lib/utils";
import { UserRes } from "@/models";
import { ColumnDef } from "@tanstack/react-table";
import { format } from "date-fns";
import { FiEdit2 } from "react-icons/fi";
import { IoCloseCircleOutline } from "react-icons/io5";
import { useNavigate } from "react-router-dom";

interface UserColumnsProps {
  handleOpenDisable: (id: string) => void;
  setOrderBy: React.Dispatch<React.SetStateAction<string>>;
  setIsDescending: React.Dispatch<React.SetStateAction<boolean>>;
  isDescending: boolean;
  orderBy: string;
}

export const userColumns = ({
  handleOpenDisable,
  setOrderBy,
  setIsDescending,
  isDescending,
  orderBy,
}: UserColumnsProps): ColumnDef<UserRes>[] => [
  {
    accessorKey: "staffCode",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
  },
  {
    accessorKey: "fullName",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
    cell: ({ row }) => {
      const user = row.original;
      return <p>{`${user.firstName} ${user.lastName}`}</p>;
    },
  },
  {
    accessorKey: "username",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
  },
  {
    accessorKey: "joinedDate",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
    cell: ({ row }) => {
      const formattedDate = format(row.original.joinedDate, "dd/MM/yyyy");
      return <p>{formattedDate}</p>;
    },
  },
  {
    accessorKey: "role",
    header: ({ column }) =>
      renderHeader(column, setOrderBy, setIsDescending, isDescending, orderBy),
    cell: ({ row }) => {
      const role = row.original.role;
      return <p>{role === 1 ? "Admin" : "Staff"}</p>;
    },
  },
  {
    accessorKey: "action",
    header: "Actions",
    cell: ({ row }) => {
      const userInfo = row.original;
      // eslint-disable-next-line react-hooks/rules-of-hooks
      const navigate = useNavigate();
      // eslint-disable-next-line react-hooks/rules-of-hooks
      const { user } = useAuth();
      if (user.id === userInfo.id) return;
      return (
        <div className="flex gap-4">
          <button
            className="text-blue-500 hover:text-blue-700"
            onClick={(e) => {
              e.stopPropagation();
              navigate(`edit/${userInfo.staffCode}`);
            }}
          >
            <FiEdit2 size={18} />
          </button>
          <button
            className="text-red-500 hover:text-red-700"
            onClick={(e) => {
              e.stopPropagation();
              handleOpenDisable(userInfo.id);
            }}
          >
            <IoCloseCircleOutline size={20} />
          </button>
        </div>
      );
    },
  },
  {
    accessorKey: "id",
    header: "",
    cell: ({ row }) => {
      const user = row.original;
      return <div className="hidden">{user.id}</div>;
    },
  },
];
