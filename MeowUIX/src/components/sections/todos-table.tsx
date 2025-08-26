"use client"

import * as React from "react"
import {
  type ColumnDef,
  flexRender,
  getCoreRowModel,
  getFilteredRowModel,
  getPaginationRowModel,
  getSortedRowModel,
  type SortingState,
  useReactTable,
  type VisibilityState,
  type ColumnFiltersState,
} from "@tanstack/react-table"
import { ArrowUpDown } from "lucide-react"

import { Input } from "@/components/ui/input"
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"
import { Dialog, DialogTrigger, DialogContent, DialogHeader, DialogTitle, DialogDescription, DialogFooter, DialogClose } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Pencil, Trash2 } from "lucide-react";
import { CreateUpdateTodoDialog } from "@/components/sections/create-update-todo-dialog";
import { deleteTodo } from "@/services/Actions/TodoService";

import type { TodoResponse, TodoStatus } from "@/services/Interfaces/TodoResponse"
import { getMyTodos } from "@/services/Actions/TodoService"

export default function DataTableTodos() {
  const [data, setData] = React.useState<TodoResponse[]>([])
  const [loading, setLoading] = React.useState(true)
  const [error, setError] = React.useState<string | null>(null)
  const [editTodo, setEditTodo] = React.useState<TodoResponse | null>(null)
  const [deleteTodoId, setDeleteTodoId] = React.useState<number | null>(null)
  const [createOpen, setCreateOpen] = React.useState(false);

  const [sorting, setSorting] = React.useState<SortingState>([])
  const [columnFilters, setColumnFilters] = React.useState<ColumnFiltersState>([])
  const [columnVisibility, setColumnVisibility] = React.useState<VisibilityState>({})
  const [rowSelection, setRowSelection] = React.useState({})

  // Refetch todos
  const fetchTodos = React.useCallback(async () => {
    setLoading(true)
    setError(null)
    const response = await getMyTodos()
    if (response.code === 200 && response.value) {
      setData(response.value)
    } else {
      setError(response.message || "Failed to fetch todos")
    }
    setLoading(false)
  }, [])

  React.useEffect(() => {
    fetchTodos()
  }, [fetchTodos])

  const columns: ColumnDef<TodoResponse>[] = [
    {
      accessorKey: "title",
      header: ({ column }) => (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          className="!pl-0"
        >
          Title
          <ArrowUpDown />
        </Button>
      ),
      cell: ({ row }) => <div className="font-medium">{row.getValue("title")}</div>,
    },
    {
      accessorKey: "description",
      header: ({ column }) => (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          className="!pl-0"
        >
          Description
          <ArrowUpDown />
        </Button>
      ),
      cell: ({ row }) => <div>{row.getValue("description")}</div>,
    },
    {
      accessorKey: "status",
      header: ({ column }) => (
        <Button
          variant="ghost"
          onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          className="!pl-0"
        >
          Status
          <ArrowUpDown />
        </Button>
      ),
      cell: ({ row }) => {
        const status: TodoStatus = row.getValue("status");

        { console.log(status) }
        let label = "";
        let color = "";
        switch (status) {
          case 0:
            label = "Pending";
            color = "text-error";
            break;
          case 1:
            label = "In Progress";
            color = "text-warning";
            break;
          case 2:
            label = "Completed";
            color = "text-success";
            break;
          case 3:
            label = "Archived";
            color = "text-muted-foreground";
            break;
          default:
            label = "Unknown";
            color = "text-muted-foreground";
        }
        return <span className={`${color} font-semibold`}>{label}</span>;
      },
    },
    // {
    //   accessorKey: "listId",
    //   header: ({ column }) => (
    //     <Button
    //       variant="ghost"
    //       onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
    //       className="!pl-0"
    //     >
    //       List
    //       <ArrowUpDown />
    //     </Button>
    //   ),
    //   cell: ({ row }) => <span>{row.getValue("listId")}</span>,
    // },
    {
      id: "actions",
      enableHiding: false,
      header: () => (
        <Button
          variant="ghost"
          className="!pl-0"
        >
          Options
        </Button>
      ),
      cell: ({ row }) => {
        const todo = row.original;
        return (
          <div className="flex gap-2">
            <Button
              variant="ghost"
              size="icon"
              title="Edit"
              onClick={() => setEditTodo(todo)}
            >
              <Pencil className="w-4 h-4" />
            </Button>
            <Button
              variant="ghost"
              size="icon"
              title="Delete"
              onClick={() => setDeleteTodoId(todo.id)}
            >
              <Trash2 className="w-4 h-4 text-red-500" />
            </Button>
          </div>
        );
      },
    },
  ];

  const table = useReactTable({
    data,
    columns,
    onSortingChange: setSorting,
    onColumnFiltersChange: setColumnFilters,
    getCoreRowModel: getCoreRowModel(),
    getPaginationRowModel: getPaginationRowModel(),
    getSortedRowModel: getSortedRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    onColumnVisibilityChange: setColumnVisibility,
    onRowSelectionChange: setRowSelection,
    state: {
      sorting,
      columnFilters,
      columnVisibility,
      rowSelection,
    },
  });

  return (
    <div className="w-full">
      <div className="flex items-center py-4">
        <Input
          placeholder="Filter by title..."
          value={(table.getColumn("title")?.getFilterValue() as string) ?? ""}
          onChange={(event) =>
            table.getColumn("title")?.setFilterValue(event.target.value)
          }
          className="max-w-sm"
        />
        <Dialog open={createOpen} onOpenChange={setCreateOpen}>
          <DialogTrigger asChild>
            <Button variant="outline" className="ml-auto">
              Create To Do
            </Button>
          </DialogTrigger>
          <CreateUpdateTodoDialog
            mode="create"
            onSuccess={() => {
              setCreateOpen(false);
              fetchTodos();
            }}
          />
        </Dialog>
      </div>
      <div className="overflow-hidden rounded-md border">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => {
                  return (
                    <TableHead key={header.id}>
                      {header.isPlaceholder
                        ? null
                        : flexRender(
                          header.column.columnDef.header,
                          header.getContext()
                        )}
                    </TableHead>
                  )
                })}
              </TableRow>
            ))}
          </TableHeader>
          <TableBody>
            {loading ? (
              <TableRow>
                <TableCell colSpan={columns.length} className="h-24 text-center">
                  Loading...
                </TableCell>
              </TableRow>
            ) : error ? (
              <TableRow>
                <TableCell colSpan={columns.length} className="h-24 text-center text-error">
                  {error}
                </TableCell>
              </TableRow>
            ) : table.getRowModel().rows?.length ? (
              table.getRowModel().rows.map((row) => (
                <TableRow key={row.id}>
                  {row.getVisibleCells().map((cell) => (
                    <TableCell key={cell.id}>
                      {flexRender(
                        cell.column.columnDef.cell,
                        cell.getContext()
                      )}
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell
                  colSpan={columns.length}
                  className="h-24 text-center"
                >
                  No results.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </div>
      {/* Edit Modal */}
      {editTodo && (
        <Dialog open={!!editTodo} onOpenChange={(open) => !open && setEditTodo(null)}>
          <CreateUpdateTodoDialog
            mode="update"
            initialData={editTodo}
            todoId={editTodo.id}
            onSuccess={() => {
              setEditTodo(null);
              fetchTodos();
            }}
          />
        </Dialog>
      )}
      {/* Delete Modal */}
      {deleteTodoId !== null && (
        <Dialog open={!!deleteTodoId} onOpenChange={(open) => !open && setDeleteTodoId(null)}>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>Delete to do</DialogTitle>
              <DialogDescription>
                Are you sure you want to delete this to do?
              </DialogDescription>
            </DialogHeader>
            <DialogFooter>
              <DialogClose asChild>
                <Button variant="outline" onClick={() => setDeleteTodoId(null)}>Cancel</Button>
              </DialogClose>
              <Button
                className="bg-red-500 hover:bg-red-600 text-white"
                onClick={async () => {
                  await deleteTodo(deleteTodoId);
                  setDeleteTodoId(null);
                  fetchTodos();
                }}
              >
                Delete
              </Button>
            </DialogFooter>
          </DialogContent>
        </Dialog>
      )}
      <div className="flex items-center justify-end space-x-2 py-4">
        <div className="text-muted-foreground flex-1 text-sm">
          {table.getFilteredRowModel().rows.length} row(s) found.
        </div>
        <div className="space-x-2">
          <Button
            variant="outline"
            size="sm"
            onClick={() => table.previousPage()}
            disabled={!table.getCanPreviousPage()}
          >
            Previous
          </Button>
          <Button
            variant="outline"
            size="sm"
            onClick={() => table.nextPage()}
            disabled={!table.getCanNextPage()}
          >
            Next
          </Button>
        </div>
      </div>
    </div>
  )
}
