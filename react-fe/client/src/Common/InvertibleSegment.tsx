import { Segment, SegmentProps } from "semantic-ui-react"
import {ApplicationContext} from "./Contexts.tsx";
import { useContext } from "react";

function InvertibleSegment(props: SegmentProps) {
    
    const applicationState = useContext(ApplicationContext).application;
    
    return (
        <Segment inverted={applicationState.isInverted} basic {...props}>
            {props.children}
        </Segment>
    )
}

export default InvertibleSegment