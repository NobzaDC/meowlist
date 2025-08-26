import {
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  DialogFooter,
  DialogClose,
} from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";

interface DeleteDialogProps {
  onDelete: () => void;
}

export function DeleteDialog({ onDelete }: DeleteDialogProps) {
  return (
    <DialogContent className="sm:max-w-[425px]">
      <DialogHeader>
        <DialogTitle>Delete to do</DialogTitle>
        <DialogDescription>
          Are you sure you want to delete this to do?
        </DialogDescription>
      </DialogHeader>
      <DialogFooter>
        <DialogClose asChild>
          <Button variant="outline">Cancel</Button>
        </DialogClose>
        <Button
          className="bg-red-500 hover:bg-red-600 text-white"
          onClick={onDelete}
        >
          Delete
        </Button>
      </DialogFooter>
    </DialogContent>
  );
}