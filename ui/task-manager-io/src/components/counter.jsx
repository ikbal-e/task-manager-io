import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { decrement, increment, incrementByAmount } from '../redux/counter-slice'
import { Button } from 'reactstrap';
import { useLazyGetDepartmentsQuery } from '../api/departments-api-slice';

export default function Counter() {

    const count = useSelector((state) => state.counter.value)
    const dispatch = useDispatch()


    // const {
    //     data,
    //     isFetching,
    //     isLoading
    // } = useGetDepartmentsQuery();

    const [getDepartments, {data: departments, isFetching}] = useLazyGetDepartmentsQuery();


    const fetch = () => {
        getDepartments();
    }

    return (
        <div>
            <div>

                <div>
                    {JSON.stringify(departments)} {isFetching ? '...refetching' : ''}
                </div>

                <Button color="danger">Danger!</Button>
                <button
                    aria-label="Increment value"
                    onClick={() => dispatch(increment())}
                >
                    Increment
                </button>
                <div id="counter">{count}</div>
                <button
                    aria-label="Decrement value"
                    onClick={() => dispatch(decrement())}
                >
                    Decrement
                </button>
                <button
                    aria-label="Increase 33"
                    onClick={() => dispatch(incrementByAmount(33))}
                >
                    Increase 33
                </button>

                <button
                    onClick={() => fetch()}
                >
                    FETCH
                </button>
            </div>
        </div>
    )
}