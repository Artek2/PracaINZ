import moment from 'moment'


export const dateFormat = (date) =>{
    return moment(date).format('DD/MM/YYYY')
}
export const dateFormat2 = (date) =>{
    return moment(date).format('YYYY-MM-DD')
}