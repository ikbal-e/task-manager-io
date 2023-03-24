
import { useEffect } from "react";
import { Link, Outlet } from "react-router-dom"
import { Table } from "reactstrap";
import { useLazyGetDepartmentsQuery } from "../api/departments-api-slice";

const DepartmentsList = () => {

    const [getDepartments, { data: departments, isFetching }] = useLazyGetDepartmentsQuery();

    useEffect(() => {
        getDepartments();
    }, []);

    return <div>
        <Table hover responsive size="sm">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Name</th>
                </tr>
            </thead>
            <tbody>
                {departments && departments.map((x) => (
                    <tr key={x.id}>
                        <td>{x.id}</td>
                        <td>{x.name}</td>
                    </tr>
                ))}
            </tbody>
        </Table>

    </div>
}

export default DepartmentsList