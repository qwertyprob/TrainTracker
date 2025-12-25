"use client";
import { ChevronLeft, ChevronRight } from "lucide-react";
import * as React from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Button } from "@/components/ui/button";

type DataRow = {
  id: number;
  name: string;
  status: string;
  date: string;
};

const sampleData: DataRow[] = [
  { id: 1, name: "Train A", status: "On Time", date: "2025-12-25" },
  { id: 2, name: "Train B", status: "Delayed", date: "2025-12-25" },
  { id: 3, name: "Train C", status: "Cancelled", date: "2025-12-25" },
  { id: 42, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 4222122, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 43222, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 422212312, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 4221231232, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 422222, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 42222, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 4333, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 4444, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 455, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 466, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 477, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 47, name: "Train D", status: "On Time", date: "2025-12-25" },
  { id: 4, name: "Train D", status: "On Time", date: "2025-12-25" },
];

export default function DataTable() {
  const [data, setData] = React.useState<DataRow[]>(sampleData);
  const [sortAsc, setSortAsc] = React.useState(true);

  const sortByName = () => {
    const sorted = [...data].sort((a, b) =>
      sortAsc ? a.name.localeCompare(b.name) : b.name.localeCompare(a.name)
    );
    setData(sorted);
    setSortAsc(!sortAsc);
  };

  return (
    <div className="p-4 bg-white rounded-2xl overflow-y-auto h-64">
      <Table>
        <TableHeader className="sticky top-0 bg-white z-10">
          <TableRow>
            <TableHead>ID</TableHead>
            <TableHead>
              <Button
                variant="ghost"
                size="sm"
                className="p-0"
                onClick={sortByName}
              >
                Name {sortAsc ? "↑" : "↓"}
              </Button>
            </TableHead>
            <TableHead>Status</TableHead>
            <TableHead>Date</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {data.map((row) => (
            <TableRow key={row.id}>
              <TableCell>{row.id}</TableCell>
              <TableCell>{row.name}</TableCell>
              <TableCell>{row.status}</TableCell>
              <TableCell>{row.date}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
}
