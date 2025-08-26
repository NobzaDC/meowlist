export type TodoStatus = 0 | 1 | 2 | 3; {/* 0: Pending, 1: InProgress, 2: Completed, 3: Archived */}

export interface TodoRequestDto {
    title: string;
    description?: string;
    listId: number;
    status?: TodoStatus;
}