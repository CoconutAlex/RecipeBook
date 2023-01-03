import axios from 'axios';
import _ from 'lodash'
import { useCallback, useEffect, useReducer, useRef, useState } from "react";
import { Grid, Search } from "semantic-ui-react";
import { Recipe } from '../models/recipe';

const initialState = {
    loading: false,
    results: [],
    value: '',
}

function exampleReducer(state: any, action: { type: string; query: string; results: any; selection: any; }) {
    switch (action.type) {
        case 'CLEAN_QUERY':
            return initialState
        case 'START_SEARCH':
            return { ...state, loading: true, value: action.query }
        case 'FINISH_SEARCH':
            return { ...state, loading: false, results: action.results }
        case 'UPDATE_SELECTION':
            return { ...state, value: action.selection }

        default:
            throw new Error()
    }
}

export default function CustomSearch() {

    const [recipes, setRecipes] = useState<Recipe[]>([]);
    useEffect(() => {
        axios.get<Recipe[]>('http://localhost:5118/Recipe/GetAllRecipes')
            .then(response => {
                setRecipes(response.data);
            })
    }, [])

    const [state, dispatch] = useReducer(exampleReducer, initialState);
    const { loading, results, value } = state;

    const timeoutRef = useRef();
    const handleSearchChange = useCallback((e: any, data: { value?: any; }) => {
        clearTimeout(timeoutRef.current)
        dispatch({
            type: 'START_SEARCH',
            query: data.value,
            results: '',
            selection: ''
        })

        setTimeout(() => {
            if (data.value.length === 0) {
                dispatch({
                    type: 'CLEAN_QUERY',
                    query: '',
                    results: '',
                    selection: ''
                })
                return
            }

            const re = new RegExp(_.escapeRegExp(data.value), 'i')
            const isMatch = (result: { title: string; }) => re.test(result.title)

            dispatch({
                type: 'FINISH_SEARCH',
                results: _.filter(recipes, isMatch),
                query: '',
                selection: ''
            })
        }, 300)
    }, [recipes])

    useEffect(() => {
        return () => {
            clearTimeout(timeoutRef.current);
        }
    }, [])

    return (
        <Grid>
            <Grid.Column width={6}>
                <Search
                    loading={loading}
                    placeholder='Search...'
                    onResultSelect={(e, data) =>
                        dispatch({
                            type: 'UPDATE_SELECTION',
                            selection: data.result.title,
                            query: '',
                            results: ''
                        })
                    }
                    onSearchChange={handleSearchChange}
                    results={results}
                    value={value}
                />
            </Grid.Column>
        </Grid>
    )
}