import {
  Table,
  TableHeader,
  TableRow,
  TableHead,
  TableBody,
  TableCell,
} from "../ui/table";
import { Skeleton } from "../ui/skeleton"; // компонент скелетона из shadcn/ui

interface SkeletonTableProps {
  rows?: number; // сколько строк показывать
  columns?: number; // сколько колонок показывать
}

export default function SkeletonTable({
  rows = 5,
  columns = 4,
}: SkeletonTableProps) {
  // Создаём массив для скелетонов
  const rowArray = Array.from({ length: rows });

  return (
    <div className="overflow-y-auto h-64 bg-white rounded-2xl p-4">
      <Table>
        <TableHeader className="sticky top-0 bg-white z-10">
          <TableRow>
            {Array.from({ length: columns }).map((_, idx) => (
              <TableHead key={idx}>
                <Skeleton className="h-4 w-16" />
              </TableHead>
            ))}
          </TableRow>
        </TableHeader>
        <TableBody>
          {rowArray.map((_, rowIdx) => (
            <TableRow key={rowIdx}>
              {Array.from({ length: columns }).map((_, colIdx) => (
                <TableCell key={colIdx}>
                  <Skeleton className="h-4 w-full" />
                </TableCell>
              ))}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
}
