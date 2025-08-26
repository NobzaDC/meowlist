import { useState } from "react";
import {
  DialogTrigger,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter,
  DialogClose,
} from "@/components/ui/dialog";
import { Label } from "@/components/ui/label";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import type { TodoRequestDto, TodoStatus } from "@/services/Interfaces/TodoRequestDto";
import { createTodo, updateTodo } from "@/services/Actions/TodoService";

interface CreateUpdateTodoDialogProps {
  mode: "create" | "update";
  initialData?: TodoRequestDto;
  todoId?: number;
  onSuccess?: () => void;
}

export function CreateUpdateTodoDialog({
  mode,
  initialData,
  todoId,
  onSuccess,
}: CreateUpdateTodoDialogProps) {
  const [title, setTitle] = useState(initialData?.title ?? "");
  const [description, setDescription] = useState(initialData?.description ?? "");
  const [listId, setListId] = useState(initialData?.listId ?? 1);
  const [status, setStatus] = useState<TodoStatus>(initialData?.status ?? 0);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async () => {
    setLoading(true);
    setError(null);

    const todo: TodoRequestDto = { title, description, listId, status };

    let response;
    if (mode === "create") {
      response = await createTodo(todo);
    } else if (mode === "update" && todoId) {
      response = await updateTodo(todoId, todo);
    }

    setLoading(false);

    console.log("Response:", response);

    if (response && (response.code === 200 || response.code === 201)) {
      if (onSuccess) onSuccess();
    } else {
      setError(response?.message || "Error saving todo");
    }
  };

  return (
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>{mode === "create" ? "Create Todo" : "Edit Todo"}</DialogTitle>
          <DialogDescription>
            {mode === "create"
              ? "Fill the form to create a new todo."
              : "Update the fields and save changes."}
          </DialogDescription>
        </DialogHeader>
        <div className="grid gap-4">
          <div className="grid gap-3">
            <Label htmlFor="title">Title</Label>
            <Input
              id="title"
              name="title"
              value={title}
              onChange={e => setTitle(e.target.value)}
              required
            />
          </div>
          <div className="grid gap-3">
            <Label htmlFor="description">Description</Label>
            <Input
              id="description"
              name="description"
              value={description}
              onChange={e => setDescription(e.target.value)}
            />
          </div>
          {/* There is not a feature for multiple lists here dont lie */}
          {/* <div className="grid gap-3">
            <Label htmlFor="listId">List ID</Label>
            <Input
              id="listId"
              name="listId"
              type="number"
              value={listId}
              onChange={e => setListId(Number(e.target.value))}
              disabled  
              required
            />
          </div> */}
          <div className="grid gap-3">
            <Label htmlFor="status">Status</Label>
            <select
              id="status"
              name="status"
              value={status}
              onChange={e => setStatus(Number(e.target.value) as TodoStatus)}
              className="border rounded px-2 py-1"
              required
            >
              <option value={0}>Pending</option>
              <option value={1}>InProgress</option>
              <option value={2}>Completed</option>
              <option value={3}>Archived</option>
            </select>
          </div>
          {error && (
            <div className="text-error text-sm text-center">{error}</div>
          )}
        </div>
        <DialogFooter>
          <DialogClose asChild>
            <Button variant="outline" type="button">Cancel</Button>
          </DialogClose>
          <Button onClick={handleSubmit} disabled={loading}>
            {loading ? "Saving..." : mode === "create" ? "Create" : "Save changes"}
          </Button>
        </DialogFooter>
      </DialogContent>
  );
}