
import { useEffect } from "react";
import { Link, Outlet } from "react-router-dom"
import { Table } from "reactstrap";
import { useLazyGetDepartmentsQuery } from "../api/departments-api-slice";
import DepartmentsList from "../components/departments-list";

const DepartmentsPage = () => {

    return <div style={{ margin: '5vh'}}>
        <DepartmentsList />

    </div>
}

export default DepartmentsPage